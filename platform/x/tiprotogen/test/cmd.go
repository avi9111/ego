package main

import (
	"fmt"

	"taiyouxi/platform/planx/util/logs"
	"taiyouxi/platform/x/tiprotogen/command"
)

func main() {
	defer logs.Close()
	d, err := command.LoadFromFile("./test.json")
	if err != nil {
		logs.Error("err %s", err.Error())
		return
	}
	fmt.Println(d)
	command.GenMultipleCSharpCode("./", d)

	command.GenGolangCodes("./", "msg", d)
}
