$(document).ready(function () {

    Vue.filter('currency', function (value) {

        var val = (value / 1).toFixed(2);
        return '$' + val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")
    });

    var VM = new Vue({
        el: '#items',
        data: {
            search: "",
            items: items
        },
        computed: {
            filteredItems() {
                return this.items.filter(item => {
                    return item.Description.toLowerCase().indexOf(this.search.toLowerCase()) > -1
                })
            },
        },
        methods: {
            setOnSell: function (id, onSell, i) {
                $.ajax({
                    type: "POST",
                    url: "/Item/SetOnSell",
                    data: {
                        "id": id,
                        "onSell": !onSell
                    },
                    success: function (result) {
                        if (result === "true") {
                            items[i].OnSell = !onSell;
                        }
                    }
                });
            },
            deleteItem: function (id, i) {

                $.confirm({
                    title: "确定要删除这个物品么？",
                    content: "删除",
                    buttons: {
                        confirm: function () {
                            $.ajax({
                                type: "POST",
                                url: "/Item/DeleteItem",
                                data: {
                                    "id": id,
                                },
                                success: function (result) {
                                    if (result === "true") {
                                        items.splice(i, 1);
                                    }
                                }
                            });
                        },
                        cancel: function () {

                        },
                    }
                });
            }
        }
    });
});