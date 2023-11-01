package rules

import (
	"taiyouxi/platform/x/youmi_imcc/base"
	"testing"

	"github.com/BurntSushi/toml"
)

func TestIsLegal(t *testing.T) {
	_, err := toml.DecodeFile(base.ConfPath, &base.Cfg)
	if err != nil {
		t.FailNow()
	}
	if isLegal("2017-02-23 10:31:42", "2017-02-23 10:31:42") == true {
		t.FailNow()
	}
	if isLegal("2017-02-23 10:32:00", "2017-02-23 10:31:40") == false {
		t.FailNow()
	}
}
