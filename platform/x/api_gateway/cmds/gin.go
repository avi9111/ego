package cmds

import (
	"taiyouxi/platform/planx/util/ginhelper"
	"taiyouxi/platform/x/api_gateway/config"

	"github.com/gin-gonic/gin"

	"taiyouxi/platform/planx/util/logs"
)

func MakeGinEngine() *gin.Engine {
	gin.SetMode(gin.ReleaseMode)
	engine := gin.New()

	ginhelper.InitSentryTags(
		config.PayCfg.SentryDSN,
		map[string]string{"service": "paygin"})

	logs.InitSentryTags(
		config.PayCfg.SentryDSN,
		map[string]string{"service": "paylog"})

	engine.Use(ginhelper.SentryGinLog())

	return engine
}
