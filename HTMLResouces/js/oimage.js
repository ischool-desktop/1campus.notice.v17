if (!RedactorPlugins) var RedactorPlugins = {};

(function($) {
    RedactorPlugins.oimage = function() {
        return {
            getTemplate: function() {
                return ['<section id="redactor-modal-oimage-insert">\
                            <label>Image URL</label>\
                            <input type="text" id="redactor-insert-oimage-input"/>\
                        </section>'].join('');
            },
            init: function() {
                var button = this.button.add('oimage', 'Insert Image');

                this.button.setAwesome('oimage', 'fa-image');

                //this.button.setAwesome('oimage', 'fa-file-image-o');
               

                this.button.addCallback(button, this.oimage.show);
            },
            show: function() {
                this.modal.addTemplate('oimage', this.oimage.getTemplate());

                this.modal.load('oimage', 'Insert Image', 700);
                this.modal.createCancelButton();

                var button = this.modal.createActionButton(this.lang.get('insert'));
                button.on('click', this.oimage.insert);

                this.selection.save();
                this.modal.show();

                $('#redactor-insert-oimage-input').focus();
            },
            insert: function() {
                var data = ['<img src="', $('#redactor-insert-oimage-input').val(), '" style="max-width:100%;" />'].join('');

                this.selection.restore();
                this.modal.close();

                var current = this.selection.getBlock() || this.selection.getCurrent();

                if (current) { 
                    $(current).after(data);
                } else {
                    this.insert.html(data);
                }

                this.code.sync();
                this.observe.images();
            }
        };
    };
})(jQuery);
