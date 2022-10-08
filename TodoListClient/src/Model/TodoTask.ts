export default interface TodoTask {
    description: string;
    id: number;
    parentId?: number;
    subTasks?: Array<TodoTask>;
    isDone: boolean;
}