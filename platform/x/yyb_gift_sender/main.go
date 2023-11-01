package main

import (
	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/x/yyb_gift_sender/config"

	"taiyouxi/platform/x/yyb_gift_sender/db"
	"taiyouxi/platform/x/yyb_gift_sender/gin_core"
)

func main() {
	defer logs.Close()
	// need BIlog?
	//logs.LoadLogConfig(logConf)

	config.LoadConfig("config.toml")

	config.LoadGiftData(config.GetDataPath() + "/yybgift.data")
	logs.Debug("Config: %v", config.CommonConfig)

	config.InitErrCode()

	db.Init()

	gin_core.Reg()

	gin_core.Run()
}
