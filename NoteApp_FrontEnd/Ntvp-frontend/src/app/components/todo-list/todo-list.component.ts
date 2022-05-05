import { ChangeDetectionStrategy,  EventEmitter, Input} from '@angular/core';
import { Output } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { NoteViewModel } from '../../models/noteModel';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TodoListComponent implements OnInit
{
  @Input()
  noteViewModels: NoteViewModel[];

  @Output()
  onRemove = new EventEmitter<number>();

  @Output()
  onEdit = new EventEmitter<NoteViewModel>();

  @Output()
  onRemoveAll = new EventEmitter();

  ngOnInit(): void {
  }

  removePost(id: number): void {
    this.onRemove.emit(id);
  }

  removeAll(): void {
    this.onRemoveAll.emit();
  }

  editPost(id: number): void {
    this.onEdit.emit(this.noteViewModels.filter(p => p.Id === id)[0]);
  }
}
