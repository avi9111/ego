package main

import (
	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/x/youmi_imcc/base"

	"github.com/gin-gonic/gin"
)

func acceptHandle(c *gin.Context) {
	netMsg := base.PlayerMsgInfoFromNet{}
	if err := c.BindJSON(&netMsg); err != nil {
		logs.Error("bind json err by %v", err)
	} else {
		if netMsg.MsgType == base.TextMsgTyp {
			base.MsgChan <- netMsg
		}
	}
	c.String(200, "success!")
}
