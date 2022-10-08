<template>
    <div class="dialog" v-if="isVisible" @click.stop="hideDialog">
        <div @click.stop class="dialog__content">
            <slot></slot>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
    setup() {
        return {}
    },
    props: {
        isVisible: Boolean,
    },
    methods: {
        hideDialog() {
            this.$emit('update:isVisible', false);
        }
    }
})
</script>

<style scoped>
.dialog {
    top: 0;
    bottom: 0;
    right: 0;
    left: 0;
    background: rgba(0, 0, 0, 0.9);
    position: fixed;
    display: flex;
}

.dialog__content {
    margin: auto;
    border-radius: 12px;
    min-height: 50px;
    min-width: 300px;
    padding: 20px;
}
</style>