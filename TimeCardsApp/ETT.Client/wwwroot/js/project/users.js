function UserLink(projectId, action, userId) {
    return '/Project/' + projectId + '/User/' + action + (isDefined(userId) ? '/' + userId : '');
}

// MAIN

function UserMain(projectId) {
    UpdateHtmlBlock(
        block = '#users',
        url = UserLink(projectId, 'Main'),
        button = '#btn-user-main',
    );
}

function UserListUpdate(projectId) {
    UpdateHtmlBlock(
        block = '#user-list',
        url = UserLink(projectId, 'List'),
        button = '#btn-user-list',
    );
}

var userSearchNow = false;
var autocompleteUsers = {};
function UserSearch(projectId, email, autocomplete, onAutocomplete) {
    if (email.length > 1 && userSearchNow === false) {
        $.ajax({
            url: UserLink(projectId, 'Search'),
            type: 'get',
            data: { email: email },
            beforeSend: function (jqXHR, settings) {
                userSearchNow = true;
            },
            success: function (data, textStatus, jqXHR) {
                for (var i = 0; i < data.length; i++) {
                    autocompleteUsers[data[i]] = null;
                }
                if (isString(autocomplete)) {
                    autocomplete = $(autocomplete);
                }
                autocomplete.autocomplete({
                    data: autocompleteUsers,
                    limit: 5,
                    onAutocomplete: onAutocomplete
                });
                //console.log(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            },
            complete: function (jqXHR, textStatus) {
                userSearchNow = false;
            }
        });
    }
}

function UserAdd(projectId, email) {
    $.ajax({
        url: UserLink(projectId, 'Add'),
        type: 'post',
        data: { email: email },
        success: function () {
            UserListUpdate(projectId);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}

// LIST

var userRoleChangingNow = false;
function UserRoleChange(projectId, userId, roleId, color) {
    if (userRoleChangingNow === false) {
        $.ajax({
            url: UserLink(projectId, 'Update', userId),
            type: 'put',
            data: { roleId: roleId },
            beforeSend: function (jqXHR, settings) {
                userRoleChangingNow = true;
            },
            success: function (data, textStatus, jqXHR) {
                $('#user-' + userId + ' span.role').each(function (i, elem) {
                    if ($(this).attr('id') === roleId.toString()) {
                        $(this).addClass(color);
                    } else {
                        $(this).removeClass(color);
                    }
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            },
            complete: function (jqXHR, textStatus) {
                userRoleChangingNow = false;
            }
        });
    }
}

var userDeletingNow = false;
function UserDelete(projectId, userId) {
    if (userDeletingNow === false) {
        $.ajax({
            url: UserLink(projectId, 'Delete', userId),
            type: 'delete',
            beforeSend: function (jqXHR, settings) {
                userDeletingNow = true;
            },
            success: function (data, textStatus, jqXHR) {
                $('#user-' + userId).remove();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            },
            complete: function (jqXHR, textStatus) {
                userDeletingNow = false;
            }
        });
    }
}
