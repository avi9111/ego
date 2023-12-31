package controllers

import (
	"errors"
	"fmt"

	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/x/auth/config"
	"taiyouxi/platform/x/auth/models"

	"github.com/astaxie/beego/httplib"
)

func BanUser(uid, gid string, time_to_ban int64, reason string) error {
	// 先封掉账号 Dynamodb封
	err := models.SetBanUnInfo(uid, time_to_ban, reason)
	if err != nil {
		return err
	}

	// 再通知login把在线的全踢掉
	return notifyLoginServerKick(uid, gid, reason, time_to_ban)
}

func notifyLoginServerKick(uid, gid, reason string, banTime int64) error {
	URL := fmt.Sprintf("%s?id=%s&gid=%s&banTime=%d&reason=%s", config.Cfg.LoginKickApiAddr, uid, gid, banTime, reason)
	var rst struct {
		Result string `json:"result"`
	}
	req := httplib.Get(URL)
	err := req.ToJSON(&rst)
	if err != nil {
		logs.Error("notifyLoginServerKick failed %s", err.Error())
		return err
	}

	rsp, _ := req.Response()
	defer rsp.Body.Close()

	if rst.Result != "ok" {
		return errors.New(rst.Result)
	}

	return nil
}
