package cmds

import (
	"taiyouxi/platform/planx/util/ginhelper"
	"taiyouxi/platform/x/tiprotogen/log"

	ucfg "taiyouxi/platform/planx/util/config"

	"github.com/gin-gonic/gin"
)

func MakeGinGMEngine() *gin.Engine {
	log.Trace("MakeGinGMEngine()")
	engine := gin.New()
	engine.Use(gin.Recovery())
	engine.Use(SentryGinLog())
	return engine
}

func MakeGinEngine() (engine *gin.Engine, exitfun func()) {
	engine = gin.New()

	engine.Use(gin.Recovery())
	engine.Use(SentryGinLog())
	//engine.Use(ginhelper.LoggerWithWriter())
	//engine.Use(ginhelper.NginxLoggerWithWriter())
	var gh gin.HandlerFunc
	var exitfunlog func()
	ucfg.NewConfig("accesslog.xml", false, func(lcfgname string, cmd ucfg.LoadCmd) {
		gh, exitfunlog = ginhelper.NgixLoggerToFile(lcfgname)
	})
	exitfun = func() {
		if exitfunlog != nil {
			exitfunlog()
		}
	}
	engine.Use(gh)
	//engine.Use(limit.CheckIdentity())
	//engine.Use(limit.RateLimit())
	//engine.Use(statsCCU())
	return engine, exitfun
}
