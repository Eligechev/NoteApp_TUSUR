
export class NoteViewModel {
  /** Идентификатор записки */
  Id: number;

  /** Название записки */
  NoteName: string;

  /** Сообщение */
  NoteMessage: string;

  /** Категория записки */
  NoteCategory: NoteCategories | null;

  CreationDate: Date;

  ChangeDate: Date;
}

export enum NoteCategories {
  Work = 0,

  Home = 1,

  HealthAndSports = 2,

  People = 3,

  Docs = 4,

  Finances = 5,

  Others = 6,
}
