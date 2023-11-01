package main

import (
	"os"
	"taiyouxi/platform/planx/util/etcd"
	"taiyouxi/platform/x/hero_h5/serverinfo"

	"github.com/gin-gonic/gin"

	//"taiyouxi/platform/x/gift_sender/config"
	"taiyouxi/platform/planx/util/logs"
	authConfig "taiyouxi/platform/x/auth/config"
	h5Config "taiyouxi/platform/x/hero_h5/config"
	"taiyouxi/platform/x/hero_h5/roleinfo"
)

func main() {

	h5Config.LoadConfig("conf/config.toml")
	err := etcd.InitClient(h5Config.CommonConfig.Etcd_Endpoint)
	if err != nil {
		logs.Error("etcd InitClient err %s", err.Error())
		logs.Critical("\n")
		logs.Close()
		os.Exit(1)
	}

	logs.Info("etcd Init client done.")
	authConfig.Cfg.Runmode = "test"
	authConfig.Cfg.EtcdRoot = h5Config.CommonConfig.Etcd_Root
	gin := gin.Default()
	serverinfo.RegisterAllServerInfo(gin)
	roleinfo.Init(gin)
	gin.Run(h5Config.CommonConfig.Port)
}
