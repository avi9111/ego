package config

import (
	"fmt"

	"taiyouxi/platform/planx/metrics"

	gm "github.com/rcrowley/go-metrics"
)

const (
	notice_prefix = "notice"
)

var (
	noticeCCU_C gm.Counter
)

func InitNoticeMetrics() {
	NewNoticeCounter := func(name string) gm.Counter {
		return metrics.NewCounter(fmt.Sprintf("%s.%s.%s", notice_prefix, metrics.GetIPToken(), name))
	}
	noticeCCU_C = NewNoticeCounter("get")
}

func AddNoticeCCUCount() {
	noticeCCU_C.Inc(1)
}
