package sdk

import (
	"fmt"
	"taiyouxi/platform/planx/util/logs"
	authConfig "taiyouxi/platform/x/auth/config"
	"time"

	"github.com/astaxie/beego/httplib"
	//"vcs.taiyouxi.net/Godeps/_workspace/src/github.com/astaxie/beego/httplib"
)

func Check6waves(token string) error {
	req := httplib.Get(authConfig.WavesSdkCfg.Url).SetTimeout(
		5000*time.Millisecond, 5000*time.Millisecond)
	req.Param("access_token", token)

	var r ret6waves
	err := req.ToJSON(&r)
	if err != nil {
		logs.Error("toJson err:%v", err)
		return err
	}

	fmt.Printf("r.result:%v", r.Result)
	if r.Result != "verified" {
		return fmt.Errorf("6waves sdk Check err, receive: %v", r)
	}
	return nil
}

type ret6waves struct {
	Result       string
	Uid          int
	Access_token string
}
