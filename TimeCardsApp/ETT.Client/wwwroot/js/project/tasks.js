function TaskLink(projectId, action, taskId) {
    return '/Project/' + projectId + '/Task/' + action + (isDefined(taskId) ? '/' + taskId : '');
}

function TaskAction(projectId, action, taskId, button, form) {
    UpdateHtmlBlock(
        block = '#tasks',
        url = TaskLink(projectId, action, taskId),
        button = button,
        preloader = null,
        form = form,
    );
}

function TaskMain(projectId) {
    TaskAction(projectId, 'Main', null, '#btn-task-main');
}
function TaskListUpdate(projectId) {
    UpdateHtmlBlock(
        block = '#task-list',
        url = TaskLink(projectId, 'List'),
        button = '#btn-task-list',
    );
}

function TaskCreate(projectId, isForm) {
    TaskAction(projectId, 'Create', null, '#btn-task-create', isForm ? '#form-task-create' : null);
}

function TaskDetails(projectId, taskId) {
    TaskAction(projectId, 'Details', taskId, '#btn-task-details');
}
function TaskEdit(projectId, taskId, isForm) {
    TaskAction(projectId, 'Edit', taskId, '#btn-task-edit', isForm ? '#form-task-edit' : null);
}
function TaskDelete(projectId, taskId, isForm) {
    TaskAction(projectId, 'Delete', taskId, '#btn-task-delete', isForm ? '#form-task-delete' : null);
}
