package base

import (
	"fmt"
	"testing"

	"time"

	"taiyouxi/platform/planx/util/logs"

	"github.com/BurntSushi/toml"
)

func TestMuteInfoRecorder_SendMail(t *testing.T) {
	_, err := toml.DecodeFile(ConfPath, &Cfg)
	if err != nil {
		return
	}
	fmt.Println(Cfg)
	mir := &MuteInfoRecorder{}
	mir.lastRecordTime = time.Now()
	//mir.SendMail()
}

func TestMuteInfoRecorder_Record(t *testing.T) {
	defer logs.Close()
	mir := &MuteInfoRecorder{}
	mir.Start()
	mir.Record(&MuteInfo{
		AcID: "Fdsfsd",
	}, time.Now())
	mir.Stop()
	mir.Start()
	mir.Record(&MuteInfo{
		AcID: "fsdsfsdfsfsdfsd",
	}, time.Now().Add(time.Hour*24))
	mir.Stop()
}
