package cmds

import (
	"log"

	//"github.com/codegangsta/cli"
	"github.com/urfave/cli"
)

var (
	commands = make(map[string]*cli.Command)
)

func GetCmdCount() (n int) {
	return len(commands)
}

// GetCommands
func InitCommands(cmds *[]cli.Command) {
	*cmds = make([]cli.Command, 0, len(commands))
	for _, v := range commands {
		*cmds = append(*cmds, *v)
	}
}

func Register(c *cli.Command) {
	if c == nil {
		return
	}

	name := c.Name
	if _, ok := commands[name]; ok {
		log.Fatalln("cmds: Register called twice for adapter " + name)
	}
	commands[name] = c
}
