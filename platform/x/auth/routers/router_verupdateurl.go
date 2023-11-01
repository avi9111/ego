package routers

import (
	"taiyouxi/platform/x/auth/config"
	"taiyouxi/platform/x/auth/controllers"
	"taiyouxi/platform/x/auth/limit"

	"github.com/gin-gonic/gin"
)

func RegVerUpdateUrl(g *gin.Engine) {
	device_id_controller := controllers.DeviceIDController{}
	// 客户端版本强制更新用
	ver_update := g.Group("/verupdate/v1/")
	ver_update.Use(limit.RateLimit())
	ver_update.GET("getUrl", device_id_controller.UpdateVer())

	tools := g.Group("/tools/")
	tools.Use(limit.RateLimit())
	tools.GET("echoip", device_id_controller.EchoIp())

	debug := g.Group("/debug/")
	debug.Use(limit.CheckIdentity(config.Spec_Header, config.Spec_Header_Content), limit.RateLimit())
	debug.POST("log", device_id_controller.DebugLog())
}
