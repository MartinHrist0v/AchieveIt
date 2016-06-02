            function OnClickImage() {
                var listsItems = $('#posts img');
                var id = 0;
                for (var i = 0; i < listsItems.length; i++) {
                    listsItems[i].addEventListener("click", function (data) {
                        id = this.parentNode.id;

                        $.ajax({
                            url: 'Post/Post?id=' + id,
                            success: function (result) {
                                $('#post').children().remove();
                                $('#post').html(result);
                                
                            }
                        })
                    });
                }
            }
            function LoadMorePosts() {
                var skipElements = 0;

                $('#view-more-posts').on("click", function (data) {
                    var usernameFromUrl = window.location.pathname;
                    var username = usernameFromUrl.substring(1, usernameFromUrl.length);
                    skipElements += 5;
                    $.ajax({
                        url: 'Post/LoadMorePosts?start=' + skipElements + '&&username=' + username,
                        success: function (result) {

                            $('#posts').append(result);
                            OnClickImage();
                            DeletePost();
                        }
                    })

                })
            };
            function DeletePost() {
                var posts = $('.delete-post');
                var id = 0;
                for (var i = 0; i < posts.length; i++) {
                    posts[i].addEventListener("click", function (data) {
                        id = this.parentNode.id;
                        $.ajax({
                            url: 'Post/DeletePost',
                            data :{id : id},
                            method: 'POST',
                            success: function(result){
                                $('#' + id).remove();
                                
                        }
                        })
                    })
                }
            }
            
            OnClickImage();
            LoadMorePosts();
            DeletePost();