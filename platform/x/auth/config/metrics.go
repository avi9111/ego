package config

import (
	"fmt"

	"taiyouxi/platform/planx/metrics"

	gm "github.com/rcrowley/go-metrics"
)

const (
	auth_prefix = "auth"
)

var (
	authRegister_C gm.Counter
	authCCU_C      gm.Counter
)

func InitAuthMetrics() {
	NewAuthCounter := func(name string) gm.Counter {
		return metrics.NewCounter(fmt.Sprintf("%s.%s.%s", auth_prefix, metrics.GetIPToken(), name))
	}
	authRegister_C = NewAuthCounter("register")
	authCCU_C = NewAuthCounter("ccu")
}

func AddAuthRegisterCount() {
	authRegister_C.Inc(1)
}

func AddAuthCCUCount() {
	authCCU_C.Inc(1)
}
