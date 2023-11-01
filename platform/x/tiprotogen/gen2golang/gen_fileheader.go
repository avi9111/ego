package gen2golang

import (
	dsl "taiyouxi/platform/x/tiprotogen/def"
	"taiyouxi/platform/x/tiprotogen/util"
)

func (g *genner2golang) genFileHeader(
	buf *util.CodeGenData,
	def *dsl.ProtoDef) {
	buf.WriteLine(headerTemplate,
		def.Name,
		def.Title,
		def.Comment)
}
