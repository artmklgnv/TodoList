<template>
    <div class="form">
        <input type="text" class="input" placeholder="Description" v-model="updateTask.description" />
        <button @click="editTask">OK</button>
        <button @click="closeWindow">Cancel</button>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TodoListApi from '../api/TodoListApi';
import UpdateTask from '../Model/UpdateTask';

export default defineComponent({
    setup() {
        return {}
    },
    props: {
        updateTask: { type: UpdateTask, required: true }
    },
    methods: {
        editTask() {
            TodoListApi.updateTask(this.updateTask)
                .then(res => this.updateTasks())
                .then(res => this.closeWindow());
        },
        closeWindow() {
            this.$emit('close');
        },
        updateTasks() {
            this.$emit('updateTasks');
        }
    }
})
</script>

<style scoped>
.input {
    min-height: 28px;
    min-width: 300px;
}

.form {
    padding: 10px;
    margin: 10px;
    display: flex;
}

button {
    margin-left: 10px;
}
</style>