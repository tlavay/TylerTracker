import { Measurements } from "./measurements";

export class Health {
  date: Date;
  weight: number;
  systolic?: number;
  diastolic?: number;
  measurements?: Measurements;
}
