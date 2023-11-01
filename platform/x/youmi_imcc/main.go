package main

import (
	"time"

	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/x/youmi_imcc/base"
	"taiyouxi/platform/x/youmi_imcc/rules"

	"github.com/BurntSushi/toml"
	"github.com/gin-gonic/gin"
)

/*
	it will discard the im data after restart server
*/

func main() {

	logs.LoadLogConfig(base.LogConfPath)

	_, err := toml.DecodeFile(base.ConfPath, &base.Cfg)
	if err != nil {
		logs.Error("load config error by %v", err)
		return
	}
	logs.Debug("load config: %v", base.Cfg)
	rules.Reg()

	time.LoadLocation("Asia/Shanghai")

	engine := gin.Default()
	engine.POST(base.Cfg.Url, acceptHandle)
	go engine.RunTLS(base.Cfg.Host, base.ServerCRT, base.ServerKEY)

	startService()
}
