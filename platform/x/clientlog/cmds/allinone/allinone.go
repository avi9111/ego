package allinone

import (
	"os"

	"taiyouxi/platform/planx/util"
	"taiyouxi/platform/planx/util/config"
	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/planx/util/signalhandler"

	"github.com/codegangsta/cli"
	"github.com/gin-gonic/gin"

	"taiyouxi/platform/x/auth/limit"
	"taiyouxi/platform/x/clientlog/cmds"
	ClientLogConfig "taiyouxi/platform/x/clientlog/config"
	"taiyouxi/platform/x/clientlog/routers"
)

func init() {
	logs.Trace("allinone cmd loaded")
	cmds.Register(&cli.Command{
		Name:   "allinone",
		Usage:  "开启所有功能",
		Action: Start,
		Flags: []cli.Flag{
			cli.StringFlag{
				Name:  "config, c",
				Value: "app.toml",
				Usage: "Onland Configuration toml config, in {CWD}/conf/ or {AppPath}/conf",
			},
		},
	})
}

var CommonCfg ClientLogConfig.CommonConfig

func Start(c *cli.Context) {
	cfgName := c.String("config")
	var common_cfg struct{ CommonConfig ClientLogConfig.CommonConfig }
	config.NewConfigToml(cfgName, &common_cfg)

	CommonCfg = common_cfg.CommonConfig
	ClientLogConfig.Cfg = common_cfg.CommonConfig

	var limit_cfg struct{ LimitConfig limit.LimitConfig }
	config.NewConfigToml(cfgName, &limit_cfg)
	limit.SetLimitCfg(limit_cfg.LimitConfig)
	limit.Init()

	if CommonCfg.IsRunModeProdAndTest() {
		gin.SetMode(gin.ReleaseMode)
	}

	var waitGroup util.WaitGroupWrapper

	cmds.InitSentry(CommonCfg.SentryDSN)

	r, exitfun := cmds.MakeGinEngine()
	defer exitfun()
	routers.RegPR(r)
	//routers.RegAuth(r)
	//routers.RegLogin(r)
	//routers.RegVerUpdateUrl(r)

	logxml := config.NewConfigPath("log.xml")
	logs.LoadLogConfig(logxml)

	util.PProfStart()

	go func() {

		if err := r.Run(CommonCfg.Httpport); err != nil { // listen and serve on 0.0.0.0:8080
			logs.Close()
			os.Exit(1)
		}

	}()

	waitGroup.Wrap(func() { signalhandler.SignalKillHandle() })
	waitGroup.Wait()
	logs.Close()
}
