package cmds

import (
	"github.com/gin-gonic/gin"
	"taiyouxi/platform/planx/util/ginhelper"
)

func InitSentry(DSN string) error {
	return ginhelper.InitSentry(DSN)
}

func SentryGinLog() gin.HandlerFunc {
	return ginhelper.SentryGinLog()
}
