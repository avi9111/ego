package mail

import (
	gmConfig "taiyouxi/platform/x/gm_tools/config"
)

var CommonCfg gmConfig.CommonConfig

func InitMail(Cfg gmConfig.CommonConfig) {
	CommonCfg = Cfg
}
