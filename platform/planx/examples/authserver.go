package main

import (
	"flag"
	"fmt"

	"taiyouxi/platform/planx/servers/game"
	// "taiyouxi/platform/planx/util/logs"
)

func main() {
	fmt.Println("PlanX Game Auth Server, version:", game.GetVersion())
	listento := flag.String("listen", ":8444", "0.0.0.0:8444, :8444")
	flag.Parse()

	server := game.NewAuthServer(*listento)
	server.Start()
}
