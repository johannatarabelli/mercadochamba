import { RandomNumberPipe } from './random-number.pipe';

describe('RandomNumberPipe', () => {
  it('create an instance', () => {
    const pipe = new RandomNumberPipe();
    expect(pipe).toBeTruthy();
  });
});
