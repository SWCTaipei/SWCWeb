
//檢查上傳檔案大小及格式 v20181025 
//輸入範例：ChkFileSize('FileUpload','10','.pdf.doc.docx');
function ChkFileSize(objId, maxSize, fileType) {
    var fileSize = 0; //檔案大小
    var SizeLimit = maxSize * 1024 * 1024;  //上傳上限，單位:byte
    var f = document.getElementById(objId);

    fileSize = f.files.item(0).size;
    fileName = f.files.item(0).name;

    str = fileName.split(".");
    extname = str[str.length - 1];
    jType = '.' + extname;

    if (fileType.indexOf(jType) < 0) {
        alert('請選擇 ' + fileType + ' 檔案格式上傳，謝謝!!');
        return false;
    }
    if (fileSize > SizeLimit) {
        alert('請選擇 ' + maxSize + 'Mb 以下檔案上傳，謝謝!!');
        return false;
    }
}

