package gen2csharp

import (
	dsl "taiyouxi/platform/x/tiprotogen/def"
	"taiyouxi/platform/x/tiprotogen/util"
)

func (g *genner2Csharp) genFunc(
	buf *util.CodeGenData,
	def *dsl.ProtoDef) {
	funcBeginStr := ""
	if def.Cheat {
		funcBeginStr = funcBeginWithCheat
	} else {
		funcBeginStr = funcBegin
	}
	buf.WriteLine(funcBeginStr,
		def.Name,
		def.Path,
		def.Name,
		def.GetReqMsgName(),
		def.Path,
		def.Name,
		def.GetRspMsgName(),
		def.Name,
		def.Title,
		def.Comment,
		def.Name,
		g.genParamCalls(def.Req.Params),
		def.GetReqMsgName())
	g.genParamsInit(buf, def.Req.Params)
	buf.WriteLine("")
	buf.WriteLine(funcEnd)
}
