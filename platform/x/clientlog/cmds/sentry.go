package cmds

import (
	"taiyouxi/platform/planx/util/ginhelper"

	"github.com/gin-gonic/gin"
)

func InitSentry(DSN string) error {
	return ginhelper.InitSentry(DSN)
}

func SentryGinLog() gin.HandlerFunc {
	return ginhelper.SentryGinLog()
}
