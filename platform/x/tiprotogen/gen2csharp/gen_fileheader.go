package gen2csharp

import (
	dsl "taiyouxi/platform/x/tiprotogen/def"
	"taiyouxi/platform/x/tiprotogen/util"
)

func (g *genner2Csharp) genFileHeader(
	buf *util.CodeGenData,
	def *dsl.ProtoDef) {
	buf.WriteLine(headerTemplate,
		def.Name,
		def.Title,
		def.Comment)
}
