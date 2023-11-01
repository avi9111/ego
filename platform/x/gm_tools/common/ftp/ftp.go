package ftp

import (
	"bytes"

	"taiyouxi/platform/planx/util/logs"

	"github.com/dutchcoders/goftp"
)

func UploadToFtp(path string, filename string, data []byte) error {
	var err error
	var ftp *goftp.FTP

	// For debug messages: goftp.ConnectDbg("ftp.server.com:21")
	if ftp, err = goftp.Connect(FtpAddress); err != nil {
		return err
	}

	defer ftp.Close()

	if err = ftp.Login(User, Passwd); err != nil {
		logs.Error("ftp.Login Err by %s", err.Error())
		return err
	}

	if err = ftp.Cwd("/"); err != nil {
		logs.Error("ftp.Cwd Err by %s", err.Error())
		return err
	}

	var curpath string
	if curpath, err = ftp.Pwd(); err != nil {
		logs.Error("ftp.Pwd Err by %s", err.Error())
		return err
	}
	logs.Trace("Current path: %s", curpath)

	r := bytes.NewReader(data)

	if err := ftp.Mkd(path); err != nil {
		logs.Error("ftp.Mkd Err by %s", err.Error())
	}

	logs.Info("Stor %s data %v", path+filename, data)

	if err := ftp.Stor(path+filename, r); err != nil {
		logs.Error("ftp.Stor Err by %s", err.Error())
		return err
	}

	return nil
}
