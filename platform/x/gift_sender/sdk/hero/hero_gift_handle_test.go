package hero

import (
	"fmt"
	"testing"

	"taiyouxi/platform/x/gift_sender/config"
)

func TestDecodeJson(t *testing.T) {
	srcData, err := DecodeData([]byte("ejJpdGVtSWhmbyI6IlJwXCJwcm9waWRcIypcIlZJX05DXCIsXCt7cm9udW1cIjoxfV0iLCJtYWlsQ29udGVudCI6Ilx1NjA2ZFx1NTU5Y1x1NjBhOFx1ODNiN1x1NWY5N1tcdTgyZjFcdTk2YzRcdTc4OGVcdTcyNDddIiwibWFpbFRpdGxlIjoiXHU4MmYxXHU5NmM0XHU0ZmYxXHU0ZTUwXHU5MGU4XHU3OThmXHU1MjI5Iiwicm9sZWlkIjo3LCJzZXJ2ZXJpZCI6MTE2fQ=="))
	if err != nil {
		t.Fail()
		return
	}
	fmt.Println("decode data:", srcData)
	giftInfo, err := ParseGiftInfoFromJson(srcData)
	if err != nil {
		fmt.Println("parseGiftInfoFromJson err by ", err)
		t.Fail()
		return
	}
	fmt.Println("giftInfo:", giftInfo)
}

func TestParseServerID(t *testing.T) {
	server, ok := ParseServerID(116)
	fmt.Println("server:", server, "ok:", ok)
}

func TestPropIDExist(t *testing.T) {
	config.LoadGameData("")
	if CheckPropExist("VI_HERO_DZ1") {
		fmt.Println("yes")
	} else {
		fmt.Println("no")
	}
}
