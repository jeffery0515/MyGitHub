var  imageType=0;
function loadingImg(selectId,category,type) {
    $.post("../../image/list", { id: category, type: type }, function (data) {
        $("#" + selectId).find("option").remove();
        $("#egg_imagedropdown_image").remove();
        imageId = $("#" + selectId).attr("imageId");
        for (i = 0; i < data.length; i++) {
           if(data[i]==imageId)
            $("#" + selectId).append("<option value=" + data[i] + " selected='selected' >../../image/index/" + data[i] + "</option>");
           else
               $("#" + selectId).append("<option value=" + data[i] + ">../../image/index/" + data[i] + "</option>");
        }
        if (type == 0) {
            $("#" + selectId).EggImageDropdown({
                height: 100,
                width: 120,
                z:1,
                dropdownHeight: 300,
                dropdownWidth:100
            });
        } else {
            $("#" + selectId).EggImageDropdown({
                height: 180,
                width: 410,
                z:1,
                dropdownHeight: 300,
                dropdownWidth: 410
            });
        }
    });
}

function uploadSuccess(file, serverData) {
    mini.hideMessageBox(messageBox);
    var data = $.parseJSON(serverData);
    if (data.success) {
        cropWindow = mini.get("cropWindow");
         cropWindow.show();
        var height;
        var width;
        if (data.type == 0) {
            height = 100;
            width = 120;
        }
        else {
            height = 180;
            width = 410;
        }
        var cropzoom = $('#cropzoom_container').cropzoom({
            width: 550,
            height: 400,
            bgColor: '#ccc',
            enableRotation: false,
            enableZoom: true,
            selector: {
                w: width,
                h: height,
                showPositionsOnDrag: false,
                showDimetionsOnDrag: false,
                centered: true,
                bgInfoLayer: '#fff',
                borderColor: 'blue',
                animated: false,
                maxWidth: width,
                maxHeight: height,
                borderColorHover: 'yellow'
            },
            image: {
                source: '../../image/index/' + data.id,
                width: data.width,
                height: data.height,
                minZoom: 30,
                maxZoom: 150
            }
        });
        $("#crop").click(function () {
            messageBox = mini.loading("正在处理图片，请稍等……");
            cropzoom.send('../../image/crop', 'POST', { id: data.id }, function (imgRet) {
                mini.hideMessageBox(messageBox);
                $("#image").attr("imageId", data.id);
               
                    loadingImg("image", $("#Category").val(), data.type);
                
                cropWindow.hide();          
            });
        });
        $("#restore").click(function () {
            $("#generated").attr("src", "");
            cropzoom.restore();
        });

    } else {
        alert(serverData);

    }
}

function fileDialogStart() {
    if (imageType == 3) {
        swfu.setPostParams({ category: "", type: 3 });
    } else {
        swfu.setPostParams({ category: $("#Category").val(), type: 3 });
    }
    var cropWindow = mini.get("cropWindow");
}
var swfSetting = {
    // Backend Settings
    upload_url: "../../image/upload",
    post_params: {},
    // File Upload Settings
    file_size_limit: "2 MB", // 2MB
    file_types: "*.jpg",
    file_types_description: "JPG Images",
    file_upload_limit: "0",
    // Event Handler Settings - these functions as defined in Handlers.js
    //  The handlers are not part of SWFUpload but are part of my website and control how
    //  my website reacts to the SWFUpload events.
    file_queue_error_handler: fileQueueError,
    file_dialog_complete_handler: fileDialogComplete,
    file_dialog_start_handler: fileDialogStart,
    //upload_progress_handler: uploadProgress,
    //upload_error_handler: uploadError,
    upload_success_handler: uploadSuccess,
    //upload_complete_handler: uploadComplete,

    // Button Settings
    button_image_url: "images/SmallSpyGlassWithTransperancy_17x18.png",
    button_placeholder_id: "upLoadButton",
    button_width: 180,
    button_height: 18,
    button_text: '上传图片',
    button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 12pt; } .buttonSmall { font-size: 10pt; }',
    button_text_top_padding: 0,
    button_text_left_padding: 18,
    button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,
    button_cursor: SWFUpload.CURSOR.HAND,
    flash_url: "../../Content/swfupload/swfupload.swf",
    custom_settings: {
        //upload_target: "divFileProgressContainer"
    },
    debug: false
};

function emptyTextInit() {
    $("input:text").each(function () {
        var emptyText = $(this).attr("emptyText");
        if (($(this).val() == "" && emptyText != "") || ($(this).val() != "" && emptyText != "" && $(this).val() == emptyText)) {
            $(this).val(emptyText);
            $(this).css("color", "silver");
        }
    });
    $("input:text").focus(function () {
        var emptyText = $(this).attr("emptyText");
        if (emptyText == $(this).val()) {
            $(this).val("");
        }
        $(this).css("color", "");
    });
    $("input:text").blur(function () {
        var emptyText = $(this).attr("emptyText");
        if ($(this).val() == "") {
            $(this).val(emptyText);
            $(this).css("color", "silver");
        }
    });
}
function tableTrInit()
{ $(".table-style tbody tr").bind("mouseover", function () { $(this).css("background-color", "#A9CBEE"); }).bind("mouseout", function () { $(this).css("background-color", "white"); }); }
function itin(tableid, day) {
    var nowRows = $("#" + tableid + " tbody tr").length;
    if (nowRows > day) {
        $("#" + tableid + " tbody tr:gt(" + (day - 1) + ")").remove();
    }
    for (var i = nowRows + 1; i <= day; i++) {
        var insertStr = "<tr><td><input type='hidden' name='itineraries[" + (i - 1) + "].orderNum' value=" + (i - 1) + " /> 第 <span class='red bold'>" + i + "</span> 天</td>"
            + "<td><textarea style='width:70px;height:120px;' name='itineraries[" + (i - 1) + "].title'></textarea></td>"
            + "<td><textarea style='width:500px;height:120px;' name='itineraries[" + (i - 1) + "].maincontent'></textarea></td>"
            + "<td><textarea style='width:50px;height:120px;' name='itineraries[" + (i - 1) + "].hotel'></textarea></td>"
            + "<td>早 <input type='checkbox' name='Itineraries[" + (i - 1) + "].HasBreakfast' value=true><input type='hidden' name='Itineraries[" + (i - 1) + "].HasBreakfast' value=false><br/>中 <input type='checkbox' name='Itineraries[" + (i - 1) + "].HasLunch' value=true><input type='hidden' name='Itineraries[" + (i - 1) + "].HasLunch' value=false><br/>晚 <input type='checkbox' name='Itineraries[" + (i - 1) + "].HasDinner' value=true><input type='hidden' name='Itineraries[" + (i - 1) + "].HasDinner' value=false><br/></td>"
            + "</tr>";
        $(insertStr).appendTo($("#" + tableid + " tbody"));
    }
    tableTrInit();
}


function lineOtherInfoOnfocus(obj) {
    var nameText = $(obj).parent().parent().attr("nameText");
    if ($(obj).parent().nextAll().length >= 1) return;
    var tempStr = "<li><input type='text' name='" + $(obj).parent().parent().attr("id") + "'   emptyText='请在这里继续输入下一条" + nameText + "'  style = 'width:670px; margin-left:20px;'  onfocus = 'lineOtherInfoOnfocus(this)'  onblur = 'lineOtherInfoBlur(this)'></li>";
    $(tempStr).appendTo($(obj).parent().parent());
    emptyTextInit();
}
function lineOtherInfoBlur(obj) {
    if ($(obj).val() == "" || ($(obj).val() == $(obj).attr("emptyText"))) {
        if ($(obj).parent().nextAll().length == 1) {
            $(obj).parent().next().remove();
            return;
        }
        if ($(obj).parent().nextAll().length >= 1 && $(obj).parent().prevAll().length == 0) {
            $(obj).parent().next().find(":text").attr("emptyText", "请在这里输入第一条" + $(obj).parent().parent().attr("nameText"));
            $(obj).parent().remove();
            return;
        }
        else {
            $(obj).parent().remove();
        }
    }
}
function addLineOtherInfo(id, str) {
    var nameText = $("#" + id).attr("nameText");
    var strs = str.split(",");
    $("#" + id + " li").remove();
    if (strs.length == 0) {

        var tempStr = "<li><input type='text'   emptyText='请在这里输入第一条" + nameText + "'  style = 'width:670px; margin-left:20px;', onfocus = 'lineOtherInfoOnfocus(this)', onblur = 'lineOtherInfoBlur(this)'></li>";
        $(tempStr).appendTo($("#" + id));
    }
    else {

        var tempStr1 = "<li><input type='text' value=" + strs[0] + " emptyText='请在这里输入第一条" + nameText + "'  style = 'width:670px; margin-left:20px;', onfocus = 'lineOtherInfoOnfocus(this)', onblur = 'lineOtherInfoBlur(this)'></li>";
        $(tempStr1).appendTo($("#" + id));
        for (var i = 1; i < strs.length; i++) {
            var tempStr2 = "<li><input type='text'  value=" + strs[i] + " emptyText='请在这里继续输入下一条" + nameText + "'  style = 'width:670px; margin-left:20px;', onfocus = 'lineOtherInfoOnfocus(this)', onblur = 'lineOtherInfoBlur(this)'></li>";
            $(tempStr2).appendTo($("#" + id));
        }
        var tempStr = "<li><input type='text'   emptyText='请在这里继续输入下一条" + nameText + "'  style = 'width:670px; margin-left:20px;', onfocus = 'lineOtherInfoOnfocus(this)', onblur = 'lineOtherInfoBlur(this)'></li>";
        $(tempStr).appendTo($("#" + id));
    }
    emptyTextInit();
}
function getOtherInfoValues(id) {

    $("#" + id).find("ul").each(function () {
        var str = "";
        $(this).find("input[type=text]").each(function () {
            if ($(this).val() != $(this).attr("emptyText"))
                str = str + $(this).val() + "|||";
        });
        var strResult = str.substr(0, str.length - 3);
        // return strResult;
        $(this).find("input[type=hidden]").val(strResult);
    });
}

function lineSubmit(formId, url) {
    $("#" + formId).find("input[type=text]").each(function () {
        if ($(this).val() == $(this).attr("emptyText"))
            $(this).val("");
    });
    // $("[name=imageNames]").val(imageNames());


    getOtherInfoValues("otherInfo");
    var data = $("#" + formId).serializeArray();

    $.post(url, data, function (r) { alert("success!") });
}




/*
function desList(departmentid) {
    $.post("@Url.Action("getbydepartment", "destination")/" + departmentid, {}, function (data) {
        $("#desList li").remove();
        for (i = 0; i < data.length; i++) {
            for (j = 0; j < data[i].des.length; j++) {
                var liStr = "<li id='des" + i + "_" + j + "'  class='multiple_li' title='" + data[i].des[j].description + "' onclick='setSelect(\"des" + i + "_" + j + "\")'   onmouseout=\"this.className='multiple_li'\" onmouseover=\"this.className='multiple_c'\">" + data[i].des[j].name + "<input id='chkdes" + i + "_" + j + "' type='checkbox'  name='DestIds'  value='" + data[i].des[j].id + "'/></li>";
                $(liStr).appendTo($("#desList"));
                $d("chkdes" + i + "_" + j).checked = false;
                try {
                    for (k = 0; k < destids.length; k++) {
                        if (data[i].des[j].id == destids[k]) {
                            $d("des" + i + "_" + j).className = "multiple_s";
                            $d("des" + i + "_" + j).onmouseover = "";
                            $d("des" + i + "_" + j).onmouseout = "";
                            $d("chkdes" + i + "_" + j).checked = true;
                        }
                    }
                } catch (e) { }

            }
        }

    });
}
*/

/*时间处理函数*/
Date.prototype.dateAdd = function (strInterval, Number) {
    var dtTmp = this;
    switch (strInterval) {
        case 's': return new Date(Date.parse(dtTmp) + (1000 * Number));
        case 'n': return new Date(Date.parse(dtTmp) + (60000 * Number));
        case 'h': return new Date(Date.parse(dtTmp) + (3600000 * Number));
        case 'd': return new Date(Date.parse(dtTmp) + (86400000 * Number));
        case 'w': return new Date(Date.parse(dtTmp) + ((86400000 * 7) * Number));
        case 'q': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number * 3, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'm': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'y': return new Date((dtTmp.getFullYear() + Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
    }
}
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "h+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
(this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
RegExp.$1.length == 1 ? o[k] :
("00" + o[k]).substr(("" + o[k]).length));
    return format;
}