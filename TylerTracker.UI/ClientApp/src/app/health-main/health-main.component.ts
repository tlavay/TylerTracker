import { AfterViewInit, Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Health } from "../models/health";
import { Measurements } from "../models/measurements";
import { WeightData } from "../models/weight-data";
import { TylerTrackerApi } from "../services/tyler-tracker-api";

@Component({
  selector: 'app-basic-example',
  templateUrl: './health-main.component.html'
})
export class HealthMainComponent implements OnInit, AfterViewInit {
  public formGroup: FormGroup;
  public currentDate: string;
  public isFormHidden: boolean;
  public loading: boolean;
  public healthData: Health[] = [];
  public weightData: any[] = [];

  constructor(private api: TylerTrackerApi) { }
  ngAfterViewInit(): void {
    this.api.getLast6MonthsOfHealthData().subscribe(result => {
      this.healthData = result;
      for (var i = 0; i < this.healthData.length; i++) {
        const health = this.healthData[i];
        this.createWeightData(health);
      }
    });
  }

  ngOnInit() {
    this.formGroup = new FormGroup({
      weight: new FormControl('', [
        Validators.required,
        Validators.pattern(/^[1-9]\d*(\.\d+)?$/gm)
      ]),
      systolic: new FormControl(),
      diastolic: new FormControl(),
      neck: new FormControl(),
      chest: new FormControl(),
      arm: new FormControl(),
      waist: new FormControl(),
    });
    this.currentDate = new Date().toLocaleDateString();
  }

  public onSubmit() {
    if (this.formGroup.valid) {
      const weight = this.formGroup.controls['weight'].value;
      const systolic = this.formGroup.controls['systolic'].value;
      const diastolic = this.formGroup.controls['diastolic'].value;
      const neck = this.formGroup.controls['neck'].value;
      const chest = this.formGroup.controls['chest'].value;
      const arm = this.formGroup.controls['arm'].value;
      const waist = this.formGroup.controls['waist'].value;

      let health: Health = {
        date: new Date(),
        weight: weight
      }

      if (systolic > 0) {
        health.systolic = systolic;
        health.diastolic = diastolic;
      }

      if (neck > 0) {
        const measurments: Measurements = {
          neck: neck,
          chest: chest,
          arm: arm,
          waist: waist
        }
        health.measurements = measurments;
      }

      this.createWeightData(health);
      console.log(this.weightData);
      this.loading = true;
      this.api.createHealthData(health).subscribe(result => {
        this.loading = false;
      });
      this.isFormHidden = true;
    }
  }

  private position: number = 0;
  private createWeightData(health: Health) {
    const blah = [health.date.toString(), health.weight];
    const myData = [];
    myData.push(blah);
    this.weightData.push(blah);
  }
}
