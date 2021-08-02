import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-line-graph',
  templateUrl: './line-graph.component.html',
})
export class LineGraphComponent  {
  @Input()
  public chartData;
  @Input()
  public chartType;
  @Input()
  public chartColumns;
  @Input()
  public chartTitle;
}
