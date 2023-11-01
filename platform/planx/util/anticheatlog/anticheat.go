package anticheatlog

import "taiyouxi/platform/planx/util/eslogger"

var PAntiCheatLogger *eslogger.ESLogger

func init() {
	PAntiCheatLogger = &eslogger.ESLogger{}
}

func Trace(accountID, typeInfo string, info interface{}, format string, v ...interface{}) {
	if PAntiCheatLogger == nil {
		return
	}
	eslogger.Trace(PAntiCheatLogger, accountID, typeInfo, info, format, v...)
}
