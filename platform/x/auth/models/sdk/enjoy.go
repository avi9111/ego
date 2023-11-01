package sdk

import (
	"fmt"
	"time"

	"taiyouxi/platform/planx/util/logs"
	authConfig "taiyouxi/platform/x/auth/config"

	"github.com/astaxie/beego/httplib"
)

func CheckEnjoy(token, uid string) error {
	req := httplib.Post(authConfig.EnjoySdkCfg.Url).SetTimeout(5*time.Second, 3*time.Second)
	req.Param("token", token)
	req.Param("uid", uid)

	var r enjoyRet
	err := req.ToJSON(&r)
	if err != nil {
		return err
	}

	logs.Debug("req url: %v", authConfig.EnjoySdkCfg.Url)
	logs.Debug("ret arg: %v", r)
	if r.RetCode != 0 {
		return fmt.Errorf("enjoy sdk CheckEnjoy err, receive: %s", r.Reason)
	}
	return nil
}

type enjoyRet struct {
	RetCode int    `json:"code"`
	Reason  string `json:"reason"`
}
