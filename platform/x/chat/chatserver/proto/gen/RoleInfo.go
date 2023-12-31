// automatically generated by the FlatBuffers compiler, do not modify

package gen

import (
	flatbuffers "github.com/google/flatbuffers/go"
)

type RoleInfo struct {
	_tab flatbuffers.Table
}

func GetRootAsRoleInfo(buf []byte, offset flatbuffers.UOffsetT) *RoleInfo {
	n := flatbuffers.GetUOffsetT(buf[offset:])
	x := &RoleInfo{}
	x.Init(buf, n+offset)
	return x
}

func (rcv *RoleInfo) Init(buf []byte, i flatbuffers.UOffsetT) {
	rcv._tab.Bytes = buf
	rcv._tab.Pos = i
}

func (rcv *RoleInfo) AccountId() []byte {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(4))
	if o != 0 {
		return rcv._tab.ByteVector(o + rcv._tab.Pos)
	}
	return nil
}

func (rcv *RoleInfo) Name() []byte {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(6))
	if o != 0 {
		return rcv._tab.ByteVector(o + rcv._tab.Pos)
	}
	return nil
}

func (rcv *RoleInfo) Level() int32 {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(8))
	if o != 0 {
		return rcv._tab.GetInt32(o + rcv._tab.Pos)
	}
	return 0
}

func (rcv *RoleInfo) MutateLevel(n int32) bool {
	return rcv._tab.MutateInt32Slot(8, n)
}

func (rcv *RoleInfo) Vip() int32 {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(10))
	if o != 0 {
		return rcv._tab.GetInt32(o + rcv._tab.Pos)
	}
	return 0
}

func (rcv *RoleInfo) MutateVip(n int32) bool {
	return rcv._tab.MutateInt32Slot(10, n)
}

func (rcv *RoleInfo) Gs() int32 {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(12))
	if o != 0 {
		return rcv._tab.GetInt32(o + rcv._tab.Pos)
	}
	return 0
}

func (rcv *RoleInfo) MutateGs(n int32) bool {
	return rcv._tab.MutateInt32Slot(12, n)
}

func (rcv *RoleInfo) Role() int32 {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(14))
	if o != 0 {
		return rcv._tab.GetInt32(o + rcv._tab.Pos)
	}
	return 0
}

func (rcv *RoleInfo) MutateRole(n int32) bool {
	return rcv._tab.MutateInt32Slot(14, n)
}

func (rcv *RoleInfo) Equips(j int) []byte {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(16))
	if o != 0 {
		a := rcv._tab.Vector(o)
		return rcv._tab.ByteVector(a + flatbuffers.UOffsetT(j*4))
	}
	return nil
}

func (rcv *RoleInfo) EquipsLength() int {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(16))
	if o != 0 {
		return rcv._tab.VectorLen(o)
	}
	return 0
}

func (rcv *RoleInfo) Guild() []byte {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(18))
	if o != 0 {
		return rcv._tab.ByteVector(o + rcv._tab.Pos)
	}
	return nil
}

func (rcv *RoleInfo) Title() []byte {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(20))
	if o != 0 {
		return rcv._tab.ByteVector(o + rcv._tab.Pos)
	}
	return nil
}

func (rcv *RoleInfo) TitleTimeout() int64 {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(22))
	if o != 0 {
		return rcv._tab.GetInt64(o + rcv._tab.Pos)
	}
	return 0
}

func (rcv *RoleInfo) MutateTitleTimeout(n int64) bool {
	return rcv._tab.MutateInt64Slot(22, n)
}

func (rcv *RoleInfo) CharStarLevel() int32 {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(24))
	if o != 0 {
		return rcv._tab.GetInt32(o + rcv._tab.Pos)
	}
	return 0
}

func (rcv *RoleInfo) MutateCharStarLevel(n int32) bool {
	return rcv._tab.MutateInt32Slot(24, n)
}

func (rcv *RoleInfo) WeaponStarLevel() int32 {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(26))
	if o != 0 {
		return rcv._tab.GetInt32(o + rcv._tab.Pos)
	}
	return 0
}

func (rcv *RoleInfo) MutateWeaponStarLevel(n int32) bool {
	return rcv._tab.MutateInt32Slot(26, n)
}

func (rcv *RoleInfo) RolePos(obj *RolePos) *RolePos {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(28))
	if o != 0 {
		x := rcv._tab.Indirect(o + rcv._tab.Pos)
		if obj == nil {
			obj = new(RolePos)
		}
		obj.Init(rcv._tab.Bytes, x)
		return obj
	}
	return nil
}

func (rcv *RoleInfo) Swing() int32 {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(30))
	if o != 0 {
		return rcv._tab.GetInt32(o + rcv._tab.Pos)
	}
	return 0
}

func (rcv *RoleInfo) MutateSwing(n int32) bool {
	return rcv._tab.MutateInt32Slot(30, n)
}

func (rcv *RoleInfo) MagicPetfigure() int32 {
	o := flatbuffers.UOffsetT(rcv._tab.Offset(32))
	if o != 0 {
		return rcv._tab.GetInt32(o + rcv._tab.Pos)
	}
	return 0
}

func (rcv *RoleInfo) MutateMagicPetfigure(n int32) bool {
	return rcv._tab.MutateInt32Slot(32, n)
}

func RoleInfoStart(builder *flatbuffers.Builder) {
	builder.StartObject(15)
}
func RoleInfoAddAccountId(builder *flatbuffers.Builder, accountId flatbuffers.UOffsetT) {
	builder.PrependUOffsetTSlot(0, flatbuffers.UOffsetT(accountId), 0)
}
func RoleInfoAddName(builder *flatbuffers.Builder, name flatbuffers.UOffsetT) {
	builder.PrependUOffsetTSlot(1, flatbuffers.UOffsetT(name), 0)
}
func RoleInfoAddLevel(builder *flatbuffers.Builder, level int32) {
	builder.PrependInt32Slot(2, level, 0)
}
func RoleInfoAddVip(builder *flatbuffers.Builder, vip int32) {
	builder.PrependInt32Slot(3, vip, 0)
}
func RoleInfoAddGs(builder *flatbuffers.Builder, gs int32) {
	builder.PrependInt32Slot(4, gs, 0)
}
func RoleInfoAddRole(builder *flatbuffers.Builder, role int32) {
	builder.PrependInt32Slot(5, role, 0)
}
func RoleInfoAddEquips(builder *flatbuffers.Builder, equips flatbuffers.UOffsetT) {
	builder.PrependUOffsetTSlot(6, flatbuffers.UOffsetT(equips), 0)
}
func RoleInfoStartEquipsVector(builder *flatbuffers.Builder, numElems int) flatbuffers.UOffsetT {
	return builder.StartVector(4, numElems, 4)
}
func RoleInfoAddGuild(builder *flatbuffers.Builder, guild flatbuffers.UOffsetT) {
	builder.PrependUOffsetTSlot(7, flatbuffers.UOffsetT(guild), 0)
}
func RoleInfoAddTitle(builder *flatbuffers.Builder, title flatbuffers.UOffsetT) {
	builder.PrependUOffsetTSlot(8, flatbuffers.UOffsetT(title), 0)
}
func RoleInfoAddTitleTimeout(builder *flatbuffers.Builder, titleTimeout int64) {
	builder.PrependInt64Slot(9, titleTimeout, 0)
}
func RoleInfoAddCharStarLevel(builder *flatbuffers.Builder, charStarLevel int32) {
	builder.PrependInt32Slot(10, charStarLevel, 0)
}
func RoleInfoAddWeaponStarLevel(builder *flatbuffers.Builder, weaponStarLevel int32) {
	builder.PrependInt32Slot(11, weaponStarLevel, 0)
}
func RoleInfoAddRolePos(builder *flatbuffers.Builder, rolePos flatbuffers.UOffsetT) {
	builder.PrependUOffsetTSlot(12, flatbuffers.UOffsetT(rolePos), 0)
}
func RoleInfoAddSwing(builder *flatbuffers.Builder, swing int32) {
	builder.PrependInt32Slot(13, swing, 0)
}
func RoleInfoAddMagicPetfigure(builder *flatbuffers.Builder, magicPetfigure int32) {
	builder.PrependInt32Slot(14, magicPetfigure, 0)
}
func RoleInfoEnd(builder *flatbuffers.Builder) flatbuffers.UOffsetT {
	return builder.EndObject()
}
