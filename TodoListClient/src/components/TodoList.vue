<template>
    <div>
        <button class="btn" @click="createSubTask(undefined)">
            <font-awesome-icon icon="fa-solid fa-plus" />
        </button>
        <DialogWindow v-model:is-visible="createDialogVisible">
            <CreateSubTask :parent-id="createTaskParentId" @close="closeCreateDialog" @update-tasks="updateAllTasks" />
        </DialogWindow>
        <DialogWindow v-model:is-visible="editDialogVisible">
            <EditTask :updateTask="updateTask" @close="closeEditDialog" @update-tasks="updateAllTasks" />
        </DialogWindow>
        <ul v-for="mainTask in mainTasks" :key="mainTask.id">
            <TaskCard :todoTask="mainTask as TodoTask" v-model:value="mainTask.isDone" @update-tasks="updateAllTasks"
                @create-task="createSubTask" @edit-task="editTask" />
        </ul>
    </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import TodoListApi from '../api/TodoListApi';
import TodoTask from '../Model/TodoTask';
import TaskCard from './TaskCard.vue';
import DialogWindow from './DialogWindow.vue';
import CreateSubTask from './CreateSubTask.vue';
import UpdateTask from '../Model/UpdateTask';
import EditTask from './EditTask.vue';

export default defineComponent({
    setup() {
        var taskDict = ref(new Map<Number, TodoTask>());
        return { taskDict };
    },
    data() {
        var mainTasks = new Array<TodoTask>();
        var createDialogVisible = false;
        var editDialogVisible = false;
        var createTaskParentId: number | undefined = 0 as number | undefined;
        var updateTask = new UpdateTask(0, "", false);
        return {
            mainTasks, createDialogVisible, createTaskParentId, editDialogVisible, updateTask
        };
    },
    mounted() {
        this.updateAllTasks();
    },
    methods: {
        closeCreateDialog() {
            this.createDialogVisible = false;
        },
        closeEditDialog() {
            this.editDialogVisible = false;
        },
        createSubTask(id: number | undefined) {
            this.createDialogVisible = true;
            this.createTaskParentId = id;
        },
        updateAllTasks() {
            TodoListApi.getMainTasks().then(tasks => {
                if (tasks != null) {
                    this.mainTasks = tasks;
                    for (const task of tasks) {
                        if (!this.taskDict.has(task.id)) {
                            this.taskDict.set(task.id, task);
                        }
                    }
                }
            });
        },
        editTask(updateTask: UpdateTask) {
            this.updateTask = updateTask;
            this.editDialogVisible = true;
        },
    },
    components: { TaskCard, DialogWindow, CreateSubTask, EditTask }
})
</script>

<style scoped>

</style>