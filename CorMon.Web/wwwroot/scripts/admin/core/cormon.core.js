
(function (window, $) {
    var $body = $(document.body);
    $html = $('html'),
    $document = $(document),
    $window = $(window),
    $globalAjaxLoading = $body.find("#global-ajax-loading");


    var LEZAN = {
        list: function () {
                var dataStore = [], listSize = 0;

                var find = function (element) {
                    for (var i = 0; i < dataStore.length; ++i) {
                        if (dataStore[i] === element) {
                            return i;
                        }
                    }
                    return -1;
                };

                var insert = function (element, after) {
                    var insertPos = find(after);
                    if (insertPos > -1) {
                        dataStore.splice(insertPos + 1, 0, element);
                        ++listSize;
                        return true;
                    }
                    return false;
                };

                var append = function (element) {
                    dataStore[listSize++] = element;
                };

                var remove = function (element) {
                    var foundAt = find(element);
                    if (foundAt > -1) {
                        dataStore.splice(foundAt, 1);
                        --listSize;
                        return true;
                    }
                    return false;
                };

                var getElements = function () {
                    return dataStore
                };

                var lenght = function () {
                    return listSize;
                };

                var clear = function () {
                    delete dataStore;
                    dataStore = [];
                    listSize = 0;
                };

                return {
                    find: find,
                    insert: insert,
                    append: append,
                    remove: remove,
                    getElements: getElements,
                    lenght: lenght,
                    clear: clear
                };
            },
        autoCompilation: function (element, url) {
                if (url === undefined) {
                    url = '/admin/Taxonomy/GetTags?search=%QUERY'
                }
                if (element === undefined) {
                    element = "#tags .typeahead";
                }
                var tags = new Bloodhound({
                    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('Name'),
                    queryTokenizer: Bloodhound.tokenizers.whitespace,
                    remote: { url: url }
                });
                tags.initialize();
                // Find element and run plugin
                $(element).typeahead({ hint: false }, {
                    name: 'tags',
                    displayKey: 'Name',
                    source: tags.ttAdapter()
                });
            },
        cropper: {
            $image: 'undefined',
            $inputImage: 'undefined',
            crope: function (aspect) {
                $image = $(".img-cropper").find("img");
                $inputImage = $('input[type="file"]');
                if (window.FileReader) {
                    $inputImage.change(function () {
                        var fileReader = new FileReader(),
                            files = this.files,
                            file;

                        if (!files.length) {
                            return;
                        }
                        file = files[0];
                        if (/^image\/\w+$/.test(file.type)) {
                            fileReader.readAsDataURL(file);
                            fileReader.onload = function () {
                                $image.cropper("reset", true).cropper("replace", this.result);
                                $inputImage.val("");
                            };
                        }
                    });
                }
                $image.cropper({
                    aspectRatio: aspect,
                    zoomable: false,
                    data: {}
                });
            },
            getImage: function ($el) {
                var img = $image.cropper("getDataURL", "image/jpeg");
                if (typeof img !== 'undefined') {
                    $('input[name=' + $el + ']').val(img);
                }
            }
        },
        editor: {
            simpleEditor: function () {
                $.FroalaEditor.DefineIcon('readmore', { NAME: 'plus' });
                $.FroalaEditor.RegisterCommand('readmore', {
                    title: 'Read More',
                    focus: false,
                    undo: false,
                    refreshAfterCallback: true,
                    callback: function () {
                        this.html.insert('--more--');
                    }
                });
                $('.content-editor').froalaEditor({
                    key: 'Cfqjjdfi1hxC10obc==',
                    enter: $.FroalaEditor.ENTER_BR,
                    toolbarButtons: ['fullscreen', 'quote', 'bold', 'italic', 'underline', 'strikeThrough', 'fontFamily', 'fontSize', 'color', 'emoticons', 'paragraphFormat', 'paragraphStyle', 'align', 'formatOL', 'formatUL', 'outdent', 'indent', 'insertTable', 'insertFile', 'undo', 'redo', 'html', 'insertLink', 'insertImage', 'insertVideo', '|', 'readmore'],
                    fileUploadURL: fileUpload,
                    imageManagerLoadURL: imagesLoad,
                    imageManagerDeleteURL: imageDelete,
                    imageUploadURL: imageUpload,
                    heightMin: 250,
                    language: 'fa',
                    placeholderText: '',
                    fontFamily: {
                        "Tahoma": "Tahoma",
                        "Arial": "Arial",
                        "Vardena": "Vardena"
                    },
                    fontSizeSelection: true,      
                    fontSize: ['8', '9', '10', '11', '12', '14', '16', '18', '21', '24', '30', '34', '36', '48', '60', '72', '96'],
                    imageStyles: {
                        blockCenter: 'وسط چین'
                    }
                });
            },
            fullEditor: function (
                imageUpload,
                imagesLoad,
                fileUpload,
                imageDelete,
                preloader,
                autoSaveURL,
                saveParam) {

                $.FroalaEditor.DefineIcon('readmore', { NAME: 'plus' });
                $.FroalaEditor.RegisterCommand('readmore', {
                    title: 'Read More',
                    focus: false,
                    undo: false,
                    refreshAfterCallback: true,
                    callback: function () {
                        this.html.insert('--more--');
                    }
                });
                $('.content-editor').froalaEditor({
                    key: 'Cfqjjdfi1hxC10obc==',
                    enter: $.FroalaEditor.ENTER_BR,
                    toolbarButtons: ['fullscreen', 'quote', 'bold', 'italic', 'underline', 'strikeThrough', 'fontFamily', 'fontSize', 'color', 'emoticons', 'paragraphFormat', 'paragraphStyle', 'align', 'formatOL', 'formatUL', 'outdent', 'indent', 'insertTable', 'insertFile', 'undo', 'redo', 'html', 'insertLink', 'insertImage', 'insertVideo', '|', 'readmore'],
                    fileUploadURL: fileUpload,
                    imageManagerLoadURL: imagesLoad,
                    imageManagerDeleteURL: imageDelete,
                    imageUploadURL: imageUpload,
                    heightMin: 250,
                    language: 'fa',
                    placeholderText: '',
                    fontFamily: {
                        "Tahoma": "Tahoma",
                        "Arial": "Arial",
                        "Vardena": "Vardena"
                    },
                    fontSizeSelection: true,
                    fontSize: ['8', '9', '10', '11', '12', '14', '16', '18', '21', '24', '30', '34', '36', '48', '60', '72', '96'],
                    imageStyles: {
                        blockCenter: 'وسط چین'
                    },
                   // autosave: false,
                   // autosaveInterval: 10000,
                    //saveParam: 'content',
                   // toolbarSticky: true,
                    direction: 'rtl',
                    //toolbarStickyOffset: 50,
                    //saveURL: autoSaveURL,
                    saveRequestType: 'POST'
                });
            },
            destroyEditor: function () {
                if ($('.content-editor').data('fa.editable')) {
                    $('.content-editor').froalaEditor('destroy');
                }
            }
        },
        dataTable: function () {

            var list = new LEZAN.list();

            // Table row select all.
            $document.on("change", ".select-all", function (e) {

                list.clear();
                if (this.checked) {
                    $(".select-row").each(function () {
                        var value = $(this).val();
                        this.checked = true;
                        list.append(value);
                    });
                } else {
                    $(".select-row").each(function () {
                        var value = $(this).val();
                        this.checked = false;
                        list.remove(value);
                    });
                }
            });

            // Table row selection.
            $document.on("change", ".select-row", function () {
                var value = $(this).val();
                if (this.checked) {
                    list.append(value);
                } else {
                    list.remove(value);
                }
            });

            // Get data list item lenght.
            var listLength = function () {
                return list.lenght();
            };
            var getList = function () {
                return list.getElements();
            };
            var clear = function () {
                list.clear();
            };

            return {
                listLength: listLength,
                getList: getList,
                clear: clear
            }
        },
        dataTableHelper: {
            rowRemove: function (el) {
                $(el).parent().parent().parent().remove();
            },
            elementRemove: function (el) {
                $(el).remove();
            }
        }
    };
    window.LEZAN = LEZAN;
    window.$body = $body;
}(window, jQuery))
