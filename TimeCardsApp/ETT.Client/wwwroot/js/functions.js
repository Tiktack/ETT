// Helpers
function isDefined(value) {
    return !(typeof value === 'undefined' || !value);
}
function isString(value) {
    return $.type(value) === 'string';
}
function isExist(block) {
    return (isString(block) ? $(block).length : block.length) > 0;
}

// Hide & Show block
function HideBlock(block) {
    if (isString(block)) { $(block).addClass('hide'); } else { block.addClass('hide'); }
}
function ShowBlock(block) {
    if (isString(block)) { $(block).removeClass('hide'); } else { block.removeClass('hide'); }
}
function IsHiddenBlock(block) {
    return isString(block) ? $(block).hasClass('hide') : block.hasClass('hide');
}
function HideOrShowBlock(block) {
    if (IsHiddenBlock(block)) { ShowBlock(block); } else { HideBlock(block); }
}
function HideBlocks(blocks) { blocks.forEach(HideBlock); }
function ShowBlocks(blocks) { blocks.forEach(ShowBlock); }
function HideOrShowBlocks(blocks) { blocks.forEach(HideOrShowBlock); }

// Disable & Enable block
function DisableBlock(block) {
    if (isString(block)) { $(block).addClass('disabled'); } else { block.addClass('disabled'); }
}
function EnableBlock(block) {
    if (isString(block)) { $(block).removeClass('disabled'); } else { block.removeClass('disabled'); }
}
function IsEnabledBlock(block) {
    return !(isString(block) ? $(block).hasClass('disabled') : block.hasClass('disabled'));
}
function DisableOrEnableBlock(block) {
    if (IsEnabledBlock(block)) { DisableBlock(block); } else { EnableBlock(block); }
}
function DisableBlocks(blocks) { blocks.forEach(DisableBlock); }
function EnableBlocks(blocks) { blocks.forEach(EnableBlock); }
function DisableOrEnableBlocks(blocks) { blocks.forEach(DisableOrEnableBlock); }

// Change html block
function HtmlBlock(block, html) {
    if (isString(block)) {
        $(block).html(html);
    } else {
        block.html(html);
    }
}

// AJAX Functions
function UpdateHtmlBlock(block, url, button, preloader, form) {
    if (isExist(block) === false) {
        console.log('Html "' + block + '" element not found.');
        return;
    }
    if (isDefined(button) && IsEnabledBlock(button) === false) {
        return;
    }
    $.ajax({
        url: url,
        data: isDefined(form) ? (isString(form) ? $(form).serialize() : form.serialize()) : null,
        type: isDefined(form) ? 'post' : 'get',
        beforeSend: function (jqXHR, settings) {
            if (isDefined(button)) {
                DisableBlock(button);
            }
            if (isDefined(preloader)) {
                HtmlBlock(block, preloader);
            }
            console.log((isDefined(form) ? 'POST' : 'GET') + ' > ' + url);
        },
        success: function (data, textStatus, jqXHR) {
            setTimeout(
                function () { HtmlBlock(block, data); },
                isDefined(preloader) ? 250 : 1
            );
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log((isDefined(form) ? 'POST' : 'GET') + ' > ' + url + ' > ' + errorThrown);
        },
        complete: function (jqXHR, textStatus) {
            if (isDefined(button)) {
                setTimeout(function () { EnableBlock(button); }, 500);
            }
        }
    });
}

﻿// Write your JavaScript functions.
