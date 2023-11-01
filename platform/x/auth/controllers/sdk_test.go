package controllers

import (
	"fmt"
	"taiyouxi/platform/x/auth/models/sdk"
	"testing"
)

func TestCheck6waves(t *testing.T) {
	err := sdk.Check6waves("abcde")
	fmt.Println(err)
}
