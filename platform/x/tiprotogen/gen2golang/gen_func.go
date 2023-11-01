package gen2golang

import (
	dsl "taiyouxi/platform/x/tiprotogen/def"
	"taiyouxi/platform/x/tiprotogen/util"
)

func (g *genner2golang) genFunc(
	buf *util.CodeGenData,
	def *dsl.ProtoDef) {
	buf.WriteLine(funcBegin,
		def.Name,
		def.Title,
		def.Comment,
		def.Name,
		def.GetReqMsgName(),
		def.GetRspMsgName(),
		def.GetPath())
	buf.WriteLine("")
	buf.WriteLine(funcEnd,
		def.Name)
}
