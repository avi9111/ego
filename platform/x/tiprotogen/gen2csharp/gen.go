package gen2csharp

import (
	dsl "taiyouxi/platform/x/tiprotogen/def"
	"taiyouxi/platform/x/tiprotogen/log"
	"taiyouxi/platform/x/tiprotogen/util"
)

type genner2Csharp struct {
}

func New() *genner2Csharp {
	return new(genner2Csharp)
}

func (g *genner2Csharp) Gen(def *dsl.ProtoDef) []byte {
	log.Trace("gen 2 charp %v", def)

	buf := util.NewCodeGenData()

	g.genFileHeader(buf, def)
	g.genReqMsg(buf, def)
	g.genRspMsg(buf, def)
	g.genNetObject(buf, def)
	g.genFunc(buf, def)

	return buf.Bytes()
}
