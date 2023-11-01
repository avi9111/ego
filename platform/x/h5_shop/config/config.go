package config

import (
	"fmt"
	"os"
	"taiyouxi/platform/planx/util/logs"

	"github.com/BurntSushi/toml"
)

type Config struct {
	EtcdRoot     string   `toml:"etcd_root"`
	EtcdEndPoint []string `toml:"etcd_endpoint"`
	Port         string   `toml:"port"`
	SecretKey    string   `toml:"secret_key"`
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
