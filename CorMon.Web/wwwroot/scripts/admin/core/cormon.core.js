
(function (window, $) {
    var $body = $(document.body);
    $html = $('html'),
    $document = $(document),
    $window = $(window),
    $globalAjaxLoading = $body.find("#global-ajax-loading");


    var CORMON = {
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
                    url = '/admin/Taxonomies/GetTags?search=%QUERY'
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
        dataTable: function () {

            var list = new CORMON.list();

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
    window.CORMON = CORMON;
    window.$body = $body;
}(window, jQuery))
