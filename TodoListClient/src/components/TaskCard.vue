<template>
    <div class="taskcard">
        <div class="taskcard__line">
            <div class="taskcard__title">
                <input type="checkbox" :checked="value" @change="check" class="checkbox" />
                <label :class="{'disabled' : value}">{{todoTask?.description}}</label>
            </div>
            <div class="taskcard__btns">
                <button class="btn" @click="deleteTask">
                    <font-awesome-icon icon="fa-solid fa-xmark" />
                </button>
                <button class="btn" @click="editTask">
                    <font-awesome-icon icon="fa-solid fa-pen-to-square" />
                </button>
                <button class="btn" @click="createTask">
                    <font-awesome-icon icon="fa-solid fa-plus" />
                </button>
            </div>
        </div>
        <div class="subtasks">
            <ul v-for="(subTasks) in todoTask?.subTasks" :key="subTasks.id">
                <TaskCard :todoTask="subTasks" v-model:value="subTasks.isDone" @update-tasks="$emit('updateTasks')"
                    @create-task="createTaskFromInternal" @edit-task="editTaskFromInternal">
                </TaskCard>
            </ul>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import TodoTask from '../Model/TodoTask';
import TodoListApi from '../api/TodoListApi';
import UpdateTask from '../Model/UpdateTask';

export default defineComponent({
    setup() {
        return {};
    },
    props: {
        todoTask: Object as () => TodoTask,
        value: Boolean
    },
    methods: {
        check() {
            this.$emit('update:value', !this.value);
            if (this.todoTask != undefined) {
                const updateTask = new UpdateTask(this.todoTask.id, this.todoTask.description, !this.value);
                TodoListApi.updateTask(updateTask);
            }
        },
        deleteTask() {
            if (this.todoTask != null) {
                TodoListApi.deleteTask(this.todoTask?.id).then(res => this.$emit('updateTasks'));
            }
        },
        createTask() {
            if (this.todoTask != null) {
                this.$emit('createTask', this.todoTask.id);
            }
        },
        createTaskFromInternal(id: number) {
            this.$emit('createTask', id);
        },
        editTask() {
            if (this.todoTask != null) {
                this.$emit('editTask', new UpdateTask(this.todoTask.id, this.todoTask.description, this.todoTask.isDone));
            }
        },
        editTaskFromInternal(updateTask: UpdateTask) {
            if (this.todoTask != null) {
                this.$emit('editTask', updateTask);
            }
        }
    }
})
</script>

<style scoped>
.disabled {
    text-decoration: line-through;
}

button {
    width: 32px;
    height: 32px;
    padding: 5px;
    margin: 5px;
}

.taskcard {
    display: flex;
    flex-direction: column;
}

.taskcard__line {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
}

label {
    max-height: 32px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    margin: 0;
    padding: 0;
    margin-left: 10px;

}

.taskcard__title {
    display: flex;
    align-items: flex-start;
    padding: 0;
    margin-right: -200px;
    align-items: center;
    width: -webkit-calc(100% - 100px);
    width: -moz-calc(100% - 100px);
    width: calc(100% - 150px);
}

.taskcard__btns {
    width: 150px;
}

.checkbox {
    width: 20px;
    height: 20px;
}

.subtasks {
    padding-left: 20px;
}
</style>