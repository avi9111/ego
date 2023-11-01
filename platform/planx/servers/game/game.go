package game

import "taiyouxi/platform/planx/servers"

type PreparePlayer func(accountid, ip string) Player

type Server interface {
	Stop()
	Start(mux *servers.Mux)
}
