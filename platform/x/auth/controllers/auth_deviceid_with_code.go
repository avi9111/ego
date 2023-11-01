package controllers

import (
	"fmt"

	"github.com/gin-gonic/gin"

	"taiyouxi/platform/planx/incode"
	"taiyouxi/platform/planx/servers/db"
	"taiyouxi/platform/planx/util/secure"
	"taiyouxi/platform/x/auth/errorctl"
	"taiyouxi/platform/x/auth/logiclog"
	"taiyouxi/platform/x/auth/models"
)

// DeviceIDController about object
/*
type DeviceIDController struct {
	PlanXController
}
*/

// RegDeviceAndLogin 注册设备ID并登录，如果已经存在则直接登录
// /auth/v1/device/:id
// TODO 如何防止撞库？安全性极低，没有合理的方案能够确保ID冲突是什么引起的
// TODO deviceID应该体现game id, did:{gid}:{deviceID}
func (dic *DeviceIDController) RegDeviceWithCodeAndLogin() gin.HandlerFunc {
	return func(c *gin.Context) {
		r := struct {
			Result      string `json:"result"`
			Authtoken   string `json:"authtoken,omitempty"`
			DisplayName string `json:"display,omitempty"`
			Err         string `json:"error,omitempty"`
		}{
			"no",
			"", "", "",
		}

		deviceID := c.Param("id")
		crc := dic.GetString(c, "crc")
		code := dic.GetString(c, "code")
		api_key := dic.GetString(c, "apikey")

		if crc == "" || deviceID == "" || code == "" || api_key == "" {
			//无法校验的请求一概放弃
			errorctl.CtrlErrorReturn(c, "[Auth.Device]",
				fmt.Errorf(":-)"), errorctl.ClientErrorFormatVerifyFailed,
			)
			return
		}

		//校验crc加密一致性
		bSafeID, _ := secure.DefaultEncode.Decode64FromNet(crc)
		safeID := string(bSafeID)
		if safeID != deviceID {
			//校验失败
			errorctl.CtrlErrorReturn(c, "[Auth.Device]",
				fmt.Errorf(":-)"), errorctl.ClientErrorFormatVerifyFailed,
			)
			return
		}

		isReg := false
		//校验成功
		// 如果设备已有用户名密码，返回错误，要求用用户名密码
		di, err, _ := models.TryToCheckDeviceInfo(deviceID)
		switch err {
		case models.XErrUserNotExist:
			// 可以创建新帐号

			// 检查注册码是否有效
			is_verify, err := incode.CheckIsCodeVerify(api_key, code)
			if err != nil {
				errorctl.CtrlErrorReturn(c, "[Auth.Device]",
					fmt.Errorf("UseCode error: error(%s)", err),
					errorctl.ClientErrorRegCodeProblem,
				)
				return
			}

			if !is_verify {
				errorctl.CtrlErrorReturn(c, "[Auth.Device]",
					fmt.Errorf("UseCode error: error(%s)", err),
					errorctl.ClientErrorRegCodeUsedProblem,
				)
				return
			}

			di, err = models.AddDevice(deviceID, "", "", "")
			// if err == nil && di == nil {
			//  err = fmt.Errorf("AddDevice got unknown result")
			// }
			uid := db.InvalidUserID
			if di != nil {
				uid = di.UserId
			}
			if err != nil {
				errorctl.CtrlErrorReturn(c, "[Auth.Device]",
					fmt.Errorf("AddDevice error: uid(%d), error(%s)", uid, err),
					errorctl.ClientErrorMaybeDBProblem,
				)
				return
			}

			// 检查注册码是否有效
			err = incode.UseCode(api_key, code)
			if err != nil {
				if err == incode.CodeHasUsed {
					errorctl.CtrlErrorReturn(c, "[Auth.Device]",
						fmt.Errorf("UseCode error: error(%s)", err),
						errorctl.ClientErrorRegCodeUsedProblem,
					)
				} else {
					errorctl.CtrlErrorReturn(c, "[Auth.Device]",
						fmt.Errorf("UseCode error: error(%s)", err),
						errorctl.ClientErrorRegCodeProblem,
					)
				}
				return
			}
			isReg = true
		case models.XErrUserExistButNeedPassword:
			//要求强制用用户名密码登录
			errorctl.CtrlErrorReturn(c, "[Auth.Device]",
				fmt.Errorf("You need use username&password to login"),
				errorctl.ClientErrorHaveToUsePassword,
			)
			return

		default:
			if err != nil {
				errorctl.CtrlErrorReturn(c, "[Auth.Device]",
					err,
					errorctl.ClientErrorMaybeDBProblem,
				)
				return
			}
		}

		//下面逻辑err == nil
		uid := di.UserId
		authToken, err := models.AuthToken(uid, deviceID)
		if err != nil {
			errorctl.CtrlErrorReturn(c, "[Auth.Device]",
				err,
				errorctl.ClientErrorMaybeDBProblem,
			)
			return
		}

		logiclog.LogLogin(uid.String(), deviceID, "", "", isReg)

		r.Result = "ok"
		r.Authtoken = authToken
		r.DisplayName = di.Display

		//Auth token 发送到登录校验服务器，并设置有效时间
		notifyLoginServer(authToken, uid)

		c.JSON(200, r)
	}
}
