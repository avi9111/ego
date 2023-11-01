package sdk

import (
	"fmt"
	"time"

	authConfig "taiyouxi/platform/x/auth/config"

	"github.com/astaxie/beego/httplib"
)

const (
	from_name = "taiyouxi"
)

func CheckVivo(token string) (error, string) {
	req := httplib.Post(authConfig.VivoSdkCfg.Url).SetTimeout(
		500*time.Millisecond, 500*time.Millisecond)
	req.Param("authtoken", token)
	req.Param("from", from_name)

	var r ret
	err := req.ToJSON(&r)
	if err != nil {
		return err, ""
	}

	if r.RetCode != 0 {
		return fmt.Errorf("vivo sdk CheckVivo err, receive: %s", r), ""
	}
	return nil, r.Data.OpenId
}

type ret struct {
	RetCode int     `json:"retcode"`
	Data    retdata `json:"data"`
}

type retdata struct {
	Success bool   `json:"success"`
	OpenId  string `json:"openid"`
}
