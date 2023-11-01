package dynamodb

import (
	"taiyouxi/platform/planx/util/awshelper"

	"github.com/cenk/backoff"
)

func NewExponentialBackOffSleepFirst() *backoff.ExponentialBackOff {
	return awshelper.NewExponentialBackOffSleepFirst()
}
