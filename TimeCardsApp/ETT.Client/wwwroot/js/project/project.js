function ProjectAction(action, id, button, form) {
    UpdateHtmlBlock(
        block = '#project',
        url = '/Project/' + action + (isDefined(id) ? ('/' + id) : ''),
        button = button,
        preloader = null,
        form = form,
    )
}

function ProjectDetails(id) {
    ProjectAction('Details', id, '#btn-project-details');
}
function ProjectEdit(id, isForm) {
    ProjectAction('Edit', id, '#btn-project-edit', (isForm ? '#form-project-edit' : null));
}
function ProjectDelete(id, isForm) {
    ProjectAction('Delete', id, '#btn-project-delete', (isForm ? '#form-project-delete' : null));
}
