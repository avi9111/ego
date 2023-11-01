package config

import (
	"fmt"
	"os"
	"taiyouxi/platform/planx/util/logs"

	"github.com/BurntSushi/toml"
)

type Config struct {
	Etcd_Root     string
	Etcd_Endpoint []string
	Port          string
	IosGid        []uint
	AndGid        []uint
}

var CommonConfig Config

func LoadConfig(configpath string) {
	if _, err := toml.DecodeFile(configpath, &CommonConfig); err != nil {
		fmt.Println(err)
		logs.Critical("Config Read Error\n")
		logs.Close()
		os.Exit(1)
	}
}
